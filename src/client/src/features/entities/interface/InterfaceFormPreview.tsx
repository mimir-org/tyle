import { InterfacePreview } from "common/components/interface";
import { getColorFromAspect } from "common/utils/getColorFromAspect";
import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { FormInterfaceLib } from "./types/formInterfaceLib";

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
