import { InfoItem } from "../../content/types/InfoItem";

export const sortInfoItems = (descriptors: InfoItem[]) => [...descriptors].sort((a, b) => a.name.localeCompare(b.name));
