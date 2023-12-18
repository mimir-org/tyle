import Root from "components/Root";
import { createRoot } from "react-dom/client";

const container = document.getElementById("root") as HTMLElement;
const root = createRoot(container);
root.render(<Root />);
