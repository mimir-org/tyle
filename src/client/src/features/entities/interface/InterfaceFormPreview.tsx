import { InterfacePreview } from "common/components/interface";
import { getColorFromAspect } from "common/utils/getColorFromAspect";
import { FormInterfaceLib } from "features/entities/interface/types/formInterfaceLib";
import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";

interface InterfaceFormPreviewProps {
  control: Control<FormInterfaceLib>;
}

export const InterfaceFormPreview = ({ control }: InterfaceFormPreviewProps) => {
  const { t } = useTranslation();
  const name = useWatch({ control, name: "name" });
  const aspect = useWatch({ control, name: "aspect" });
  const aspectColor = getColorFromAspect(aspect);
  const terminalColor = useWatch({ control, name: "terminalColor" });

  return (
    <InterfacePreview
      variant={"large"}
      name={name ? name : t("interface.name")}
      aspectColor={aspectColor}
      interfaceColor={terminalColor}
    />
  );
};
