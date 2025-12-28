# pet-carx – workflow & git guide

## 1. Branch workflow (bắt buộc tuân theo)

### Luồng chuẩn: check/winform → dev → main

### Ý nghĩa từng nhánh

- `check`  
  - Tất cả code **sql, database**
  - Create table, constraint, trigger, procedure, index, procedure,...
  - **Không merge thẳng**, phải qua pull request (PR)

- `dev`  
  - Nhánh tích hợp
  - Nhận code từ `check`, `winform`
  - Test ổn mới đẩy tiếp

- `main`  
  - Nhánh ổn định
  - Chỉ merge khi đã ok toàn bộ
  - Dùng để nộp bài / demo

⚠️ Dode SQL **không** push thẳng vào `dev` hoặc `main` mà phải push vào `check`, tạo PR rồi chờ duyệt.

---

## 2. Clone repo lần đầu

```bash
git clone <repo-url>
cd pet-carx
git branch
```

Nếu chưa thấy branch:

```bash
git fetch --all
```

---

## 3. Làm việc với sql (nhánh `check`)
### Bước 1. Chuyển sang `check`

```bash
git switch check
git pull origin check
```

### Bước 2. Code

- Viết chữ thường, **không** in hoa
- Comment rõ ràng
  
### Bước 3. Commit

```bash
git add .
git commit -m "feat(db): description"
```

### Bước 4. Push

```bash
git push origin check
```

### Bước 5. Tạo PR
- base: `dev`
- compare: `check`
  
---

## 4. Sửa tiếp code trong cùng PR
```bash
git switch check
git pull origin check
```

Sửa code → commit → push tiếp:
```bash
git add .
git commit -m "fix(db): description"
git push origin check
```
  
---

## 5. Làm việc với winform

```bash
git switch winform
git pull origin winform
```

Đẩy code UI qua → commit → push

```bash
git add .
git commit -m "feat(winform): decription"
git push origin winform
```

Sau đó tạo PR:
- base: `dev`
- compare: `winform`

---

## 6. Cập nhật branch trước khi code (rất quan trọng)
Trước khi code tiếp, luôn chạy:
```bash
git pull --rebase origin <branch>
```

---

## 7. Rebase khi `dev` đã có thay đổi

Nếu `dev` mới được merge mà `check` đang cũ:

```bash
git switch check
git fetch origin
git rebase origin/dev
```

Nếu có conflict:
- Sửa file
- `git add <file>`
- `git rebase --continue`
