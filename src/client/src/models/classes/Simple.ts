import { Attribute } from "..";

export const SIMPLE_KIND = "Simple";

class Simple {
  id: string;
  name: string;
  semanticReference: string;
  attributes: Attribute[];
  nodeId: string;

  kind: string = SIMPLE_KIND;

  constructor(simple: Simple) {
    Object.assign(this, simple);
  }
}

export default Simple;
