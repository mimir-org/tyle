import { UnitLibCm } from "@mimirorg/typelibrary-types";
import { SelectItem } from "../../content/types/SelectItem";

export const mapUnitLibCmToSelectItem = (unit: UnitLibCm): SelectItem => ({
  id: unit.id,
  name: unit.name,
  traits: {
    description: unit.description,
  },
});
