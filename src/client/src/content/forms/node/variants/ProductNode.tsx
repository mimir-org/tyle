import { Control, UseFormRegister } from "react-hook-form";
import { Aspect } from "../../../../models/tyle/enums/aspect";
import { FormNodeLib } from "../../types/formNodeLib";
import { NodeFormAttributes } from "../attributes/NodeFormAttributes";
import { NodeFormTerminals } from "../terminals/NodeFormTerminals";

export interface ProductNodeProps {
  control: Control<FormNodeLib>;
  register: UseFormRegister<FormNodeLib>;
}

export const ProductNode = ({ control, register }: ProductNodeProps) => {
  return (
    <>
      <NodeFormTerminals control={control} />
      <NodeFormAttributes control={control} register={register} aspects={[Aspect.Product]} />
    </>
  );
};
