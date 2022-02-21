import { Attribute } from "./index";

export const SIMPLE_KIND = "Simple";

interface Simple {
  id: string;
  name: string;
  semanticReference: string;
  attributes: Attribute[];
  nodeId: string;
  kind: string;
}

export default Simple;
