import { AttributeLibCm } from "@mimirorg/typelibrary-types";

export const isAttributeLibCm = (item: unknown): item is AttributeLibCm =>
  (<AttributeLibCm>item).kind === "AttributeLibCm";
