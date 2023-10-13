import { ReferenceSource } from "./referenceSource";

export interface RdlObject {
  id: number;
  name: string;
  description?: string;
  iri: string;
  source: ReferenceSource;
}
