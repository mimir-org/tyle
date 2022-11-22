import { InfoItem } from "common/types/infoItem";

export const sortInfoItems = (descriptors: InfoItem[]) => [...descriptors].sort((a, b) => a.name.localeCompare(b.name));
