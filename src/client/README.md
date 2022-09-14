# :TYLE Client

## Code style

The project uses ESLint to enforce code style. This is governed by the .eslintrc.js file in client/.

To run linting checks against the codebase:

```bash
npm run lint
```

Create react app integrates with ESLint, and will throw warnings/errors on build, if there are style warnings/errors.

### VSCode Eslint integration

If using vscode, use the ESLint (by Microsoft) extension to integrate ESLint check into vscode.

## Code formatting

The project uses Prettier to enforce code formatting. This is governed by the .prettierrc file in the 'client/' root.

To format the codebase, run:

```bash
npm run format
```

To check for formatting issues (dry-run), run:

```bash
npm run format:check
```

### VSCode Prettier extension

If using vscode, use the Prettier - Code formatter (by Prettier) extension to incorporate prettier into vscode. Additionally, you can configure code formatting when saving files via preferences:

1.  Ctrl + Shift + P to open vscode command prompt.
2.  Select "Preferences: Open Settings (JSON)"
3.  Set formatting on save to true

```txt
"editor.formatOnSave": true
```
