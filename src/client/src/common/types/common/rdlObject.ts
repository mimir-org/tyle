import { ReferenceSource } from "./referenceSource";

export interface RdlObject {
  id: number;
  name: string;
  description: string | undefined;
  iri: string;
  source: ReferenceSource;
}
