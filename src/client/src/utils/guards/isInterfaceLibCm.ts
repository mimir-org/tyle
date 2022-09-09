import { InterfaceLibCm } from "@mimirorg/typelibrary-types";

export const isInterfaceLibCm = (item: unknown): item is InterfaceLibCm =>
  (<InterfaceLibCm>item).kind === "InterfaceLibCm";
