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
const MAX_BODY_BYTES = 16 * 1024;
fs.mkdirSync(DATA_DIR, { recursive: true });
const db = new DatabaseSync(path.join(DATA_DIR, 'tasks.sqlite'));
db.exec(`
  PRAGMA journal_mode = WAL;
  CREATE TABLE IF NOT EXISTS tasks (
    id TEXT PRIMARY KEY,
    title TEXT NOT NULL CHECK(length(title) BETWEEN 1 AND 120),
    description TEXT NOT NULL DEFAULT '' CHECK(length(description) <= 500),
    status TEXT NOT NULL CHECK(status IN ('todo', 'in_progress', 'done')),
    priority TEXT NOT NULL CHECK(priority IN ('low', 'normal', 'high')),
    due_date TEXT,
    created_at TEXT NOT NULL,
    updated_at TEXT NOT NULL
  );
`);
const mimeTypes = { '.html': 'text/html; charset=utf-8', '.css': 'text/css; charset=utf-8', '.js': 'text/javascript; charset=utf-8' };
function sendJson(response, status, data) {
  response.writeHead(status, { 'Content-Type': 'application/json; charset=utf-8', 'Cache-Control': 'no-store' });
  response.end(JSON.stringify(data));
}
function getTasks() {
  return db.prepare(`SELECT id,title,description,status,priority,due_date AS dueDate,created_at AS createdAt,updated_at AS updatedAt FROM tasks ORDER BY CASE status WHEN 'in_progress' THEN 0 WHEN 'todo' THEN 1 ELSE 2 END, CASE priority WHEN 'high' THEN 0 WHEN 'normal' THEN 1 ELSE 2 END, CASE WHEN due_date IS NULL THEN 1 ELSE 0 END,due_date,created_at DESC`).all();
}
function getStats() {
  const row = db.prepare(`SELECT COUNT(*) AS total,SUM(CASE WHEN status='todo' THEN 1 ELSE 0 END) AS todo,SUM(CASE WHEN status='in_progress' THEN 1 ELSE 0 END) AS inProgress,SUM(CASE WHEN status='done' THEN 1 ELSE 0 END) AS done FROM tasks`).get();
  const total = Number(row.total), done = Number(row.done || 0);
  return { total, todo: Number(row.todo || 0), inProgress: Number(row.inProgress || 0), done, completionRate: total ? Math.round(done * 100 / total) : 0 };
}
async function readJson(request) {
  let size = 0; const chunks = [];
  for await (const chunk of request) {
    size += chunk.length;
    if (size > MAX_BODY_BYTES) throw Object.assign(new Error('Dữ liệu gửi lên quá lớn.'), { statusCode: 413 });
    chunks.push(chunk);
  }
  if (!size) return {};
  try { return JSON.parse(Buffer.concat(chunks).toString('utf8')); }
  catch { throw Object.assign(new Error('Nội dung JSON không hợp lệ.'), { statusCode: 400 }); }
}
function validateTask(input, existing = {}) {
  const title = typeof input.title === 'string' ? input.title.trim() : existing.title;
  const description = typeof input.description === 'string' ? input.description.trim() : (existing.description || '');
  const status = input.status === undefined ? (existing.status || 'todo') : input.status;
  const priority = input.priority === undefined ? (existing.priority || 'normal') : input.priority;
  const dueDate = input.dueDate === undefined ? (existing.dueDate || null) : input.dueDate;
  if (typeof title !== 'string' || title.length < 1 || title.length > 120) throw Object.assign(new Error('Tên công việc cần từ 1 đến 120 ký tự.'), { statusCode: 400 });
  if (typeof description !== 'string' || description.length > 500) throw Object.assign(new Error('Mô tả tối đa 500 ký tự.'), { statusCode: 400 });
  if (!['todo','in_progress','done'].includes(status)) throw Object.assign(new Error('Trạng thái không hợp lệ.'), { statusCode: 400 });
  if (!['low','normal','high'].includes(priority)) throw Object.assign(new Error('Mức ưu tiên không hợp lệ.'), { statusCode: 400 });
  if (dueDate !== null && (typeof dueDate !== 'string' || !/^\d{4}-\d{2}-\d{2}$/.test(dueDate) || Number.isNaN(Date.parse(`${dueDate}T00:00:00Z`)) || new Date(`${dueDate}T00:00:00Z`).toISOString().slice(0,10) !== dueDate)) throw Object.assign(new Error('Hạn hoàn thành không hợp lệ.'), { statusCode: 400 });
  return { title, description, status, priority, dueDate };
}
const getTask = db.prepare('SELECT id,title,description,status,priority,due_date AS dueDate,created_at AS createdAt,updated_at AS updatedAt FROM tasks WHERE id=?');
async function handleApi(request, response, url) {
  const parts = url.pathname.split('/').filter(Boolean);
  if (url.pathname === '/api/tasks' && request.method === 'GET') return sendJson(response, 200, { tasks: getTasks(), stats: getStats() });
  if (url.pathname === '/api/tasks' && request.method === 'POST') {
    const v = validateTask(await readJson(request)), now = new Date().toISOString(), id = randomUUID();
    db.prepare('INSERT INTO tasks (id,title,description,status,priority,due_date,created_at,updated_at) VALUES (?,?,?,?,?,?,?,?)').run(id,v.title,v.description,v.status,v.priority,v.dueDate,now,now);
    return sendJson(response, 201, { task: getTask.get(id) });
  }
  if (parts[0] === 'api' && parts[1] === 'tasks' && parts.length === 3) {
    const id = parts[2], existing = getTask.get(id);
    if (!existing) return sendJson(response,404,{error:'Không tìm thấy công việc.'});
    if (request.method === 'PUT' || request.method === 'PATCH') {
      const v = validateTask(await readJson(request),existing);
      db.prepare('UPDATE tasks SET title=?,description=?,status=?,priority=?,due_date=?,updated_at=? WHERE id=?').run(v.title,v.description,v.status,v.priority,v.dueDate,new Date().toISOString(),id);
      return sendJson(response,200,{task:getTask.get(id)});
    }
    if (request.method === 'DELETE') { db.prepare('DELETE FROM tasks WHERE id=?').run(id); response.writeHead(204); return response.end(); }
  }
  return sendJson(response,404,{error:'Không tìm thấy đường dẫn API.'});
}
function serveStatic(response, pathname) {
  let relative;
  try { relative = pathname === '/' ? 'Bai1.html' : decodeURIComponent(pathname.slice(1)); }
  catch { return sendJson(response,400,{error:'Địa chỉ yêu cầu không hợp lệ.'}); }
  const file = path.resolve(PUBLIC_DIR,relative);
  if (!file.startsWith(PUBLIC_DIR + path.sep)) return sendJson(response,403,{error:'Không có quyền truy cập.'});
  fs.readFile(file,(error,content) => {
    if (error) return sendJson(response,error.code === 'ENOENT' ? 404 : 500,{error:error.code === 'ENOENT' ? 'Không tìm thấy tệp.' : 'Không thể đọc tệp.'});
    response.writeHead(200,{'Content-Type':mimeTypes[path.extname(file)] || 'application/octet-stream','Cache-Control':'no-cache'});
    response.end(content);
  });
}
const server = http.createServer(async (request,response) => {
  let url;
  try { url = new URL(request.url,`http://${request.headers.host || 'localhost'}`); }
  catch { return sendJson(response,400,{error:'Địa chỉ yêu cầu không hợp lệ.'}); }
  try {
    if (url.pathname.startsWith('/api/')) return await handleApi(request,response,url);
    if (request.method !== 'GET' && request.method !== 'HEAD') return sendJson(response,405,{error:'Phương thức không được hỗ trợ.'});
    return serveStatic(response,url.pathname);
  } catch (error) {
    console.error('Request failed:',error.message);
    return sendJson(response,error.statusCode || 500,{error:error.statusCode ? error.message : 'Có lỗi khi xử lý yêu cầu.'});
  }
});
server.on('error',error => { console.error(`Không thể mở cổng ${PORT}: ${error.message}`); process.exitCode=1; db.close(); });
server.listen(PORT,HOST,() => { console.log(`Focus Todo đang chạy tại http://${HOST}:${PORT}`); console.log(`SQLite: ${path.relative(ROOT,path.join(DATA_DIR,'tasks.sqlite'))}`); });
for (const signal of ['SIGINT','SIGTERM']) process.on(signal,() => server.close(() => { db.close(); process.exit(0); }));
