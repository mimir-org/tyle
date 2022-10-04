import { TypeReferenceSub } from "@mimirorg/typelibrary-types";
import { useEffect, useRef } from "react";
import { Control, UseFieldArrayReturn, useWatch } from "react-hook-form";
import { Text } from "../../../../complib/text";
import { InfoItem } from "../../../types/InfoItem";
import { ValueObject } from "../../types/valueObject";
import { HasUnitsAndReferences } from "./FormUnits";

export const showAttributeUnits = (referenceSubs?: TypeReferenceSub[]) => referenceSubs && referenceSubs.length > 0;

export const onAddUnits = (ids: string[], unitFields: UseFieldArrayReturn<HasUnitsAndReferences, "unitIdList">) => {
  ids.forEach((id) => {
    const unitHasNotBeenAdded = !unitFields.fields.some((f) => f.value === id);
    if (unitHasNotBeenAdded) {
      unitFields.append({ value: id });
    }
  });
};

export const useTypeReferenceSubsAsUnits = (
  control: Control<HasUnitsAndReferences>,
  replace: (values: ValueObject<string>[]) => void
) => {
  const references = useWatch({ control, name: "typeReferences" });
  const referenceSubs = useRef<TypeReferenceSub[]>([]);

  useEffect(() => {
    referenceSubs.current = references?.flatMap((x) => x.subs) ?? [];
    const units = referenceSubs.current.map((x) => ({ value: x.id }));
    replace(units);
  }, [references, replace]);

  return referenceSubs.current;
};

export const getSelectItemsFromTypeReferenceSubs = (referenceSubs?: TypeReferenceSub[]) => {
  if (!referenceSubs || referenceSubs.length == 0) return [];

  const mapped: InfoItem[] = referenceSubs.map((x) => ({
    id: x.id,
    name: x.name,
    descriptors: {
      IRI: (
        <Text
          as={"a"}
          href={x.iri}
          target={"_blank"}
          rel={"noopener noreferrer"}
          variant={"body-small"}
          color={"inherit"}
        >
          {x.iri}
        </Text>
      ),
    },
  }));

  return mapped;
};
