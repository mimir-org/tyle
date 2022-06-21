import { Control, useWatch } from "react-hook-form";
import { useGetTerminals } from "../../../data/queries/tyle/queriesTerminal";
import { getColorFromAspect } from "../../../utils/getColorFromAspect";
import { NodePreview } from "../../home/components/about/components/node/NodePreview";
import { FormNodeLib } from "../types/formNodeLib";
import { getTerminalItemsFromFormData } from "./NodeFormPreview.helpers";

interface NodeFormPreviewProps {
  control: Control<FormNodeLib>;
}

export const NodeFormPreview = ({ control }: NodeFormPreviewProps) => {
  const terminalQuery = useGetTerminals();

  const name = useWatch({ control, name: "name" });
  const symbol = useWatch({ control, name: "symbol" });
  const aspect = useWatch({ control, name: "aspect" });
  const nodeTerminals = useWatch({ control, name: "nodeTerminals" });

  return (
    <NodePreview
      variant={"large"}
      name={name}
      img={symbol}
      color={getColorFromAspect(aspect)}
      terminals={getTerminalItemsFromFormData(nodeTerminals, terminalQuery.data)}
    />
  );
};
