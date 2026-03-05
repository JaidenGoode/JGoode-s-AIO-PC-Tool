# JGoode's AIO PC Tool

This project is a **Node + Express + React (Vite)** application.
The framework is fine for this kind of app — the most common startup issue is usually command order (running `start` before a production build exists) or a port conflict.

## Quick start

1. Install dependencies:
   ```bash
   npm install
   ```
2. Run in development mode (recommended while editing):
   ```bash
   npm run dev
   ```
3. Open:
   - `http://localhost:5000`

## Production-style start

You can now run:

```bash
npm run start
```

`start` automatically runs a build first (`prestart`) so it won't fail with `Cannot find module dist/index.cjs` anymore.

## Troubleshooting “app not opening”

### 1) Port 5000 is already in use
If you see `EADDRINUSE`, another process already has the port.

Run with a different port:
```bash
PORT=5050 npm run dev
```
Then open `http://localhost:5050`.

### 2) You launched `start` without building
This is now handled automatically by `prestart`, but if you want to do it manually:
```bash
npm run build
npm run start:prod
```

### 3) Blank page / stale frontend assets
Do a clean rebuild:
```bash
rm -rf dist
npm run build
npm run start:prod
```

## Useful scripts

- `npm run dev` — development server with Vite integration.
- `npm run build` — builds client and server into `dist/`.
- `npm run start` — production start (auto-build first).
- `npm run start:prod` — start existing production build only.
- `npm run check` — TypeScript type checking.
