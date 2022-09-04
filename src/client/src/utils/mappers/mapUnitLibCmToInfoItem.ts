import { UnitLibCm } from "@mimirorg/typelibrary-types";
import { InfoItem } from "../../content/types/InfoItem";

export const mapUnitLibCmToInfoItem = (unit: UnitLibCm): InfoItem => ({
  id: unit.id,
  name: unit.name,
  descriptors: {
    description: unit.description,
  },
});
