import { QuantityDatumLibAm } from "@mimirorg/typelibrary-types";
import { Control, useWatch } from "react-hook-form";
import QuantityDatumPreview from "./QuantityDatumPreview";
import { StyledFormPreviewDiv } from "../FormPreviewContainer";

interface QuantityDatumFormPreviewProps {
  control: Control<QuantityDatumLibAm>;
  small?: boolean;
}

export const QuantityDatumFormPreview = ({ control, small }: QuantityDatumFormPreviewProps): JSX.Element => {
  const name = useWatch({ control, name: "name" });
  const description = useWatch({ control, name: "description" });
  const quantityDatumType = useWatch({ control, name: "quantityDatumType" });

  return (
    <StyledFormPreviewDiv>
      <QuantityDatumPreview
        name={name ? name : ""}
        description={description}
        quantityDatumType={quantityDatumType}
        small={small}
      />
    </StyledFormPreviewDiv>
  );
};
