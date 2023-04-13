import { AspectObjectItem } from "common/types/aspectObjectItem";

export const isAspectObjectItem = (item: unknown): item is AspectObjectItem =>
  (<AspectObjectItem>item).kind === "AspectObjectItem";
