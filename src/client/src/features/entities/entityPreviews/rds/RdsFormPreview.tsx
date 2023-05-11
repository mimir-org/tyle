import { RdsLibCm } from "@mimirorg/typelibrary-types";
import { Control, useWatch } from "react-hook-form";
import { RdsPreview } from "./RdsPreview";

interface RdsFormPreviewProps {
  control: Control<RdsLibCm>;
  small?: boolean;
}

export const RdsFormPreview = ({ control, small }: RdsFormPreviewProps): JSX.Element => {
  const name = useWatch({ control, name: "name" });
  const description = useWatch({ control, name: "description" });
  const rdsCode = useWatch({ control, name: "rdsCode" });

  return <RdsPreview name={name ? name : "RDS"} description={description} rdsCode={rdsCode} small={small} />;
};
