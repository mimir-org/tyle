import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useGetTerminals } from "../../../data/queries/tyle/queriesTerminal";
import { getColorFromAspect } from "../../../utils/getColorFromAspect";
import { NodePreview } from "../../common/node";
import { getTerminalItemsFromFormData } from "./NodeFormPreview.helpers";
import { FormNodeLib } from "./types/formNodeLib";

interface NodeFormPreviewProps {
  control: Control<FormNodeLib>;
}

export const NodeFormPreview = ({ control }: NodeFormPreviewProps) => {
  const { t } = useTranslation();
  const terminalQuery = useGetTerminals();

  const name = useWatch({ control, name: "name" });
  const symbol = useWatch({ control, name: "symbol" });
  const aspect = useWatch({ control, name: "aspect" });
  const nodeTerminals = useWatch({ control, name: "nodeTerminals" });

  return (
    <NodePreview
      variant={"large"}
      name={name ? name : t("node.name")}
      img={symbol}
      color={getColorFromAspect(aspect)}
      terminals={getTerminalItemsFromFormData(nodeTerminals, terminalQuery.data)}
    />
  );
};
