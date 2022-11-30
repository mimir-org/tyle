import { getColorFromAspect } from "common/utils/getColorFromAspect";
import { useGetTerminals } from "external/sources/terminal/terminal.queries";
import { NodePreview } from "features/common/node";
import { getTerminalItemsFromFormData } from "features/entities/node/NodeFormPreview.helpers";
import { FormNodeLib } from "features/entities/node/types/formNodeLib";
import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";

interface NodeFormPreviewProps {
  control: Control<FormNodeLib>;
}

export const NodeFormPreview = ({ control }: NodeFormPreviewProps) => {
  const { t } = useTranslation("entities");
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
