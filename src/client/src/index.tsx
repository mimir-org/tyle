import { Root } from "components/Root/Root";
import { createRoot } from "react-dom/client";
import "./i18n";

const container = document.getElementById("root") as HTMLElement;
const root = createRoot(container);
root.render(<Root />);
