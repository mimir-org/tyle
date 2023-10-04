import { ReferenceSource } from "./referenceSource";

export interface RdlObject {
    id: number;
    name: string;
    description: string | null;
    iri: string;
    source: ReferenceSource;
}