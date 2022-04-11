import { NodeLibCm } from "./nodeLibCm";
import { InterfaceLibCm } from "./interfaceLibCm";
import { TransportLibCm } from "./transportLibCm";

export interface LibraryTypeLibCm {
  nodes: NodeLibCm[];
  interfaces: InterfaceLibCm[];
  transports: TransportLibCm[];
  kind: string;
}
