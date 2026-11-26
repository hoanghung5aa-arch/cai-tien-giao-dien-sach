const http = require('node:http');
const fs = require('node:fs');
const path = require('node:path');
const { randomUUID } = require('node:crypto');
const { DatabaseSync } = require('node:sqlite');

const ROOT = __dirname;
const PUBLIC_DIR = path.join(ROOT, 'public');
const DATA_DIR = path.join(ROOT, 'data');
const PORT = Number(process.env.PORT || 8080);
const HOST = process.env.HOST || '127.0.0.1';
const MAX_BODY_BYTES = 20 * 1024;
const STATUSES = ['pending', 'shipping', 'completed', 'cancelled'];
fs.mkdirSync(DATA_DIR, { recursive: true });
const db = new DatabaseSync(path.join(DATA_DIR, 'orders.sqlite'));
db.exec(`
  PRAGMA journal_mode = WAL;
  CREATE TABLE IF NOT EXISTS orders (
    id TEXT PRIMARY KEY,
    order_code TEXT NOT NULL UNIQUE,
    customer_name TEXT NOT NULL CHECK(length(customer_name) BETWEEN 1 AND 100),
    phone TEXT NOT NULL CHECK(length(phone) BETWEEN 8 AND 20),
    product_summary TEXT NOT NULL CHECK(length(product_summary) BETWEEN 1 AND 200),
    amount INTEGER NOT NULL CHECK(amount >= 0),
    status TEXT NOT NULL CHECK(status IN ('pending','shipping','completed','cancelled')),
    note TEXT NOT NULL DEFAULT '' CHECK(length(note) <= 500),
    created_at TEXT NOT NULL,
    updated_at TEXT NOT NULL
  );
`);
const mimeTypes = { '.html': 'text/html; charset=utf-8', '.css': 'text/css; charset=utf-8', '.js': 'text/javascript; charset=utf-8' };
function json(res, status, data) { res.writeHead(status, { 'Content-Type': 'application/json; charset=utf-8', 'Cache-Control': 'no-store' }); res.end(JSON.stringify(data)); }
const ORDER_SELECT = `SELECT id,order_code AS orderCode,customer_name AS customerName,phone,product_summary AS productSummary,amount,status,note,created_at AS createdAt,updated_at AS updatedAt FROM orders`;
const selectOrder = db.prepare(ORDER_SELECT);
function listOrders() { return db.prepare(`${ORDER_SELECT} ORDER BY created_at DESC`).all(); }
function stats() {
 const rows = db.prepare(`SELECT status,COUNT(*) AS count FROM orders GROUP BY status`).all();
 const counts = Object.fromEntries(STATUSES.map(s => [s,0])); for (const row of rows) counts[row.status] = Number(row.count);
 const revenue = Number(db.prepare(`SELECT COALESCE(SUM(amount),0) AS total FROM orders WHERE status='completed'`).get().total);
 return { total: Object.values(counts).reduce((a,b)=>a+b,0), counts, completedRevenue: revenue };
}
async function readJson(req) {
 let size=0; const chunks=[];
 for await (const chunk of req) { size+=chunk.length; if(size>MAX_BODY_BYTES) throw Object.assign(new Error('Dữ liệu gửi lên quá lớn.'),{statusCode:413}); chunks.push(chunk); }
 if(!size) return {};
 try { return JSON.parse(Buffer.concat(chunks).toString('utf8')); } catch { throw Object.assign(new Error('Dữ liệu JSON không hợp lệ.'),{statusCode:400}); }
}
function validate(input, old={}) {
 const value=(key, fallback='')=> input[key]===undefined ? (old[key] ?? fallback) : input[key];
 const customerName=typeof value('customerName')==='string'?value('customerName').trim():'';
 const phone=typeof value('phone')==='string'?value('phone').trim():'';
 const productSummary=typeof value('productSummary')==='string'?value('productSummary').trim():'';
 const status=value('status', 'pending'); const note=typeof value('note')==='string'?value('note').trim():'';
 const amount=value('amount');
 if(!customerName||customerName.length>100) throw Object.assign(new Error('Tên khách hàng từ 1 đến 100 ký tự.'),{statusCode:400});
 if(!/^\+?[0-9\s().-]{8,20}$/.test(phone)) throw Object.assign(new Error('Số điện thoại cần từ 8 đến 20 ký tự số.'),{statusCode:400});
 if(!productSummary||productSummary.length>200) throw Object.assign(new Error('Thông tin sản phẩm từ 1 đến 200 ký tự.'),{statusCode:400});
 if(!Number.isSafeInteger(Number(amount))||Number(amount)<0||Number(amount)>1000000000000) throw Object.assign(new Error('Tổng tiền không hợp lệ.'),{statusCode:400});
 if(!STATUSES.includes(status)) throw Object.assign(new Error('Trạng thái đơn hàng không hợp lệ.'),{statusCode:400});
 if(note.length>500) throw Object.assign(new Error('Ghi chú tối đa 500 ký tự.'),{statusCode:400});
 return {customerName,phone,productSummary,amount:Number(amount),status,note};
}
function one(id) { return db.prepare(`${ORDER_SELECT} WHERE id=?`).get(id); }
function nextCode() { const day=new Date().toISOString().slice(2,10).replaceAll('-',''); return `DH${day}-${String(Date.now()).slice(-5)}`; }
async function api(req,res,url) {
 const parts=url.pathname.split('/').filter(Boolean);
 if(url.pathname==='/api/orders'&&req.method==='GET') return json(res,200,{orders:listOrders(),stats:stats()});
 if(url.pathname==='/api/orders'&&req.method==='POST') {
  const v=validate(await readJson(req)),now=new Date().toISOString(),id=randomUUID(),code=nextCode();
  db.prepare('INSERT INTO orders(id,order_code,customer_name,phone,product_summary,amount,status,note,created_at,updated_at) VALUES(?,?,?,?,?,?,?,?,?,?)').run(id,code,v.customerName,v.phone,v.productSummary,v.amount,v.status,v.note,now,now);
  return json(res,201,{order:one(id)});
 }
 if(parts[0]==='api'&&parts[1]==='orders'&&parts.length===3) {
  const id=parts[2],old=one(id); if(!old) return json(res,404,{error:'Không tìm thấy đơn hàng.'});
  if(req.method==='PUT') { const v=validate(await readJson(req),old),now=new Date().toISOString(); db.prepare('UPDATE orders SET customer_name=?,phone=?,product_summary=?,amount=?,status=?,note=?,updated_at=? WHERE id=?').run(v.customerName,v.phone,v.productSummary,v.amount,v.status,v.note,now,id); return json(res,200,{order:one(id)}); }
  if(req.method==='PATCH') { const {status}=await readJson(req); if(!STATUSES.includes(status)) return json(res,400,{error:'Trạng thái đơn hàng không hợp lệ.'}); db.prepare('UPDATE orders SET status=?,updated_at=? WHERE id=?').run(status,new Date().toISOString(),id); return json(res,200,{order:one(id)}); }
  if(req.method==='DELETE') { db.prepare('DELETE FROM orders WHERE id=?').run(id); res.writeHead(204); return res.end(); }
 }
 return json(res,404,{error:'Không tìm thấy API.'});
}
function serve(res,pathname) {
 let rel; try { rel=pathname==='/'?'Bai1.html':decodeURIComponent(pathname.slice(1)); } catch { return json(res,400,{error:'Địa chỉ không hợp lệ.'}); }
 const file=path.resolve(PUBLIC_DIR,rel); if(!file.startsWith(PUBLIC_DIR+path.sep)) return json(res,403,{error:'Không có quyền truy cập.'});
 fs.readFile(file,(err,data)=>{ if(err) return json(res,err.code==='ENOENT'?404:500,{error:'Không thể đọc tệp.'}); res.writeHead(200,{'Content-Type':mimeTypes[path.extname(file)]||'application/octet-stream','Cache-Control':'no-cache'}); res.end(data); });
}
const server=http.createServer(async(req,res)=>{ let url; try{url=new URL(req.url,`http://${req.headers.host||'localhost'}`); if(url.pathname.startsWith('/api/')) return await api(req,res,url); if(req.method!=='GET'&&req.method!=='HEAD') return json(res,405,{error:'Phương thức không được hỗ trợ.'}); return serve(res,url.pathname);}catch(e){console.error(e.message);return json(res,e.statusCode||500,{error:e.statusCode?e.message:'Có lỗi xử lý yêu cầu.'});} });
server.on('error',e=>{console.error(`Không mở được cổng ${PORT}: ${e.message}`);process.exitCode=1;db.close();});
server.listen(PORT,HOST,()=>console.log(`Quản lý đơn hàng: http://${HOST}:${PORT}\nSQLite: data/orders.sqlite`));
for(const signal of ['SIGINT','SIGTERM']) process.on(signal,()=>server.close(()=>{db.close();process.exit(0);}));


