import { createRoot } from "react-dom/client";
import { Root } from "./content/root";
import "./i18n";

const container = document.getElementById("root") as HTMLElement;
const root = createRoot(container);
root.render(<Root />);
