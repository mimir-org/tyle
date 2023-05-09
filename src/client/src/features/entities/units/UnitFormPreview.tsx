import { Control, useWatch } from "react-hook-form";
import UnitPreview from "./UnitPreview";
import { UnitLibAm } from "@mimirorg/typelibrary-types";

interface UnitFormPreviewProps {
  control: Control<UnitLibAm>;
}

export const UnitFormPreview = ({ control }: UnitFormPreviewProps) => {
  const name = useWatch({ control, name: "name" });
  const description = useWatch({ control, name: "description" });
  const symbol = useWatch({ control, name: "symbol" });

  return <UnitPreview unit={{ name, description, symbol }} />;
};
