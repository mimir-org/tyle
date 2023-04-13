import { AspectObjectLibCm } from "@mimirorg/typelibrary-types";

export const isAspectObjectLibCm = (item: unknown): item is AspectObjectLibCm => (<AspectObjectLibCm>item).kind === "AspectObjectLibCm";
