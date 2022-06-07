import { Control, UseFormRegister } from "react-hook-form";
import { Aspect } from "../../../../models/tyle/enums/aspect";
import { FormNodeLib } from "../../types/formNodeLib";
import { NodeFormAttributes } from "../attributes/NodeFormAttributes";
import { NodeFormPredefinedAttributes } from "../predefined-attributes/NodeFormPredefinedAttributes";

export interface LocationNodeProps {
  control: Control<FormNodeLib>;
  register: UseFormRegister<FormNodeLib>;
}

export const LocationNode = ({ control, register }: LocationNodeProps) => {
  return (
    <>
      <NodeFormAttributes control={control} aspects={[Aspect.Location]} />
      <NodeFormPredefinedAttributes control={control} register={register} aspects={[Aspect.Location]} />
    </>
  );
};
