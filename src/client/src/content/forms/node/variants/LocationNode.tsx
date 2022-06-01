import { Control } from "react-hook-form";
import { FormNodeLib } from "../../types/formNodeLib";
import { NodeFormAttributes } from "../NodeFormAttributes";
import { Aspect } from "../../../../models/tyle/enums/aspect";

export interface LocationNodeProps {
  control: Control<FormNodeLib>;
}

export const LocationNode = ({ control }: LocationNodeProps) => {
  return (
    <>
      <NodeFormAttributes control={control} aspects={[Aspect.Location]} />
    </>
  );
};
