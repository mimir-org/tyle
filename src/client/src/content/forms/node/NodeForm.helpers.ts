import { useEffect, useState } from "react";
import { DefaultValues, KeepStateOptions, UnpackNestedValue } from "react-hook-form";
import { useParams } from "react-router-dom";
import { useGetNode } from "../../../data/queries/tyle/queriesNode";
import { NodeTerminalLibAm } from "../../../models/tyle/application/nodeTerminalLibAm";
import { TerminalLibCm } from "../../../models/tyle/client/terminalLibCm";
import { Aspect } from "../../../models/tyle/enums/aspect";
import { ConnectorDirection } from "../../../models/tyle/enums/connectorDirection";
import { TerminalItem } from "../../home/types/TerminalItem";
import { FormNodeLib, mapNodeLibCmToFormNodeLibAm } from "../types/formNodeLib";

export const getTerminalItemsFromFormData = (formTerminals: NodeTerminalLibAm[], sourceTerminals?: TerminalLibCm[]) => {
  if (!sourceTerminals || sourceTerminals.length < 1) {
    return [];
  }

  const terminalItems: TerminalItem[] = [];

  formTerminals.forEach((formTerminal) => {
    const sourceTerminal = sourceTerminals.find((x) => x.id === formTerminal.terminalId);

    sourceTerminal &&
      terminalItems.push({
        name: sourceTerminal.name,
        color: sourceTerminal.color,
        amount: formTerminal.number,
        direction: ConnectorDirection[formTerminal.connectorDirection] as keyof typeof ConnectorDirection,
      });
  });

  return terminalItems;
};

export const aspectOptions = [
  { value: Aspect.None, label: "None" },
  { value: Aspect.NotSet, label: "NotSet" },
  { value: Aspect.Location, label: "Location" },
  { value: Aspect.Function, label: "Function" },
  { value: Aspect.Product, label: "Product" },
];

/**
 * Hook ties together params from react router, node data from react query and react hook form binding
 *
 * @param reset function which takes node data as parameter and populates form
 */
export const usePrefilledNodeData = (
  reset: (
    values?: DefaultValues<FormNodeLib> | UnpackNestedValue<FormNodeLib>,
    keepStateOptions?: KeepStateOptions
  ) => void
) => {
  const { id } = useParams();
  const nodeQuery = useGetNode(id);
  const [hasPrefilled, setHasPrefilled] = useState(false);

  useEffect(() => {
    if (!hasPrefilled && nodeQuery.isSuccess) {
      setHasPrefilled(true);
      reset(mapNodeLibCmToFormNodeLibAm(nodeQuery.data), { keepDefaultValues: false });
    }
  }, [hasPrefilled, nodeQuery.isSuccess, nodeQuery.data, reset]);

  return hasPrefilled;
};

/**
 * Resets the part of node form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: (value: keyof FormNodeLib) => void) => {
  resetField("selectedAttributePredefined");
  resetField("nodeTerminals");
  resetField("attributeIdList");
};
