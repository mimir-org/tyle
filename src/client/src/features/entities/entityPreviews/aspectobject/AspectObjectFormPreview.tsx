import { getColorFromAspect } from "common/utils/getColorFromAspect";
import { useGetTerminals } from "external/sources/terminal/terminal.queries";
import { AspectObjectPreview } from "features/common/aspectobject";
import { getTerminalItemsFromFormData } from "features/entities/entityPreviews/aspectobject/AspectObjectFormPreview.helpers";
import { FormAspectObjectLib } from "features/entities/aspectobject/types/formAspectObjectLib";
import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";

interface AspectObjectFormPreviewProps {
  control: Control<FormAspectObjectLib>;
}

export const AspectObjectFormPreview = ({ control }: AspectObjectFormPreviewProps) => {
  const { t } = useTranslation("entities");
  const terminalQuery = useGetTerminals();

  const name = useWatch({ control, name: "name" });
  const symbol = useWatch({ control, name: "symbol" });
  const aspect = useWatch({ control, name: "aspect" });
  const aspectObjectTerminals = useWatch({ control, name: "aspectObjectTerminals" });

  return (
    <AspectObjectPreview
      variant={"large"}
      name={name ? name : t("aspectObject.name")}
      img={symbol}
      color={getColorFromAspect(aspect)}
      terminals={getTerminalItemsFromFormData(aspectObjectTerminals, terminalQuery.data)}
    />
  );
};
