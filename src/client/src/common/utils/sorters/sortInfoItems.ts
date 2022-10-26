import { InfoItem } from "../../types/infoItem";

export const sortInfoItems = (descriptors: InfoItem[]) => [...descriptors].sort((a, b) => a.name.localeCompare(b.name));
