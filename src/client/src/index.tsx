import { createRoot } from "react-dom/client";
import { Root } from "./features/Root";
import "./i18n";

const container = document.getElementById("root") as HTMLElement;
const root = createRoot(container);
root.render(<Root />);
