import { RouteObject } from "react-router-dom";
import { TerminalForm } from "../../../../forms/terminal/TerminalForm";

export const terminalFormRoutes: RouteObject[] = [
  { path: "form/terminal", element: <TerminalForm /> },
  { path: "form/terminal/clone/:id", element: <TerminalForm /> },
  { path: "form/terminal/edit/:id", element: <TerminalForm isEdit /> },
];
