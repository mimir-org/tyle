import { UnitLibCm } from "@mimirorg/typelibrary-types";
import { UseFieldArrayReturn } from "react-hook-form";
import { mapUnitLibCmToInfoItem } from "../../../../utils/mappers/mapUnitLibCmToInfoItem";
import { FormAttributeLib } from "../types/formAttributeLib";

export const onAddUnits = (ids: string[], unitFields: UseFieldArrayReturn<FormAttributeLib, "unitIdList">) => {
  ids.forEach((id) => {
    const unitHasNotBeenAdded = !unitFields.fields.some((f) => f.value === id);
    if (unitHasNotBeenAdded) {
      unitFields.append({ value: id });
    }
  });
};

export const getSelectItemsFromUnitsLibCms = (units?: UnitLibCm[]) => {
  if (!units || units.length == 0) return [];

  return units.map((x) => mapUnitLibCmToInfoItem(x));
};
