import { TerminalForm } from "features/entities/terminal/TerminalForm";
import { RouteObject } from "react-router-dom";

export const terminalFormBasePath = "form/terminal";

export const terminalFormRoutes: RouteObject[] = [
  { path: terminalFormBasePath, element: <TerminalForm /> },
  { path: `${terminalFormBasePath}/clone/:id`, element: <TerminalForm mode={"clone"} /> },
  { path: `${terminalFormBasePath}/edit/:id`, element: <TerminalForm mode={"edit"} /> },
];
