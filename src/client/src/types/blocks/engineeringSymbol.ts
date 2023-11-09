import { ConnectionPoint } from "./connectionPoint";

export interface EngineeringSymbol {
  id: number;
  label: string;
  iri: string;
  description?: string;
  path: string;
  height: number;
  width: number;
  connectionPoints: ConnectionPoint[];
}
