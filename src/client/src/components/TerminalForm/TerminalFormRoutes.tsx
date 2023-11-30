import { RouteObject } from "react-router-dom";
import TerminalForm2 from "./TerminalForm2";

export const terminalFormBasePath = "form/terminal";

export const terminalFormRoutes: RouteObject[] = [
  { path: terminalFormBasePath, element: <TerminalForm2 /> },
  { path: `${terminalFormBasePath}/clone/:id`, element: <TerminalForm2 mode={"clone"} /> },
  { path: `${terminalFormBasePath}/edit/:id`, element: <TerminalForm2 mode={"edit"} /> },
];
