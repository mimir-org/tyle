import { RouteObject } from "react-router-dom";
import { TerminalForm } from "./TerminalForm";

export const terminalFormRoutes: RouteObject[] = [
  { path: "form/terminal", element: <TerminalForm /> },
  { path: "form/terminal/clone/:id", element: <TerminalForm mode={"clone"} /> },
  { path: "form/terminal/edit/:id", element: <TerminalForm mode={"edit"} /> },
];
