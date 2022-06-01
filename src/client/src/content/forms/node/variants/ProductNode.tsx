import { Control } from "react-hook-form";
import { FormNodeLib } from "../../types/formNodeLib";
import { NodeFormAttributes } from "../NodeFormAttributes";
import { Aspect } from "../../../../models/tyle/enums/aspect";
import { NodeFormTerminals } from "../NodeFormTerminals";

export interface ProductNodeProps {
  control: Control<FormNodeLib>;
}

export const ProductNode = ({ control }: ProductNodeProps) => {
  return (
    <>
      <NodeFormTerminals control={control} />
      <NodeFormAttributes control={control} aspects={[Aspect.Product]} />
    </>
  );
};
