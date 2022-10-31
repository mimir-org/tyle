import { TransportPreview } from "common/components/transport";
import { getColorFromAspect } from "common/utils/getColorFromAspect";
import { FormTransportLib } from "features/entities/transport/types/formTransportLib";
import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";

interface TransportFormPreviewProps {
  control: Control<FormTransportLib>;
}

export const TransportFormPreview = ({ control }: TransportFormPreviewProps) => {
  const { t } = useTranslation();
  const name = useWatch({ control, name: "name" });
  const aspect = useWatch({ control, name: "aspect" });
  const aspectColor = getColorFromAspect(aspect);
  const terminalColor = useWatch({ control, name: "terminalColor" });

  return (
    <TransportPreview
      variant={"large"}
      name={name ? name : t("terminal.name")}
      aspectColor={aspectColor}
      transportColor={terminalColor}
    />
  );
};
