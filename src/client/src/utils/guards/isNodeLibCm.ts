import { NodeLibCm } from "@mimirorg/typelibrary-types";

export const isNodeLibCm = (item: unknown): item is NodeLibCm => (<NodeLibCm>item).kind === "NodeLibCm";
