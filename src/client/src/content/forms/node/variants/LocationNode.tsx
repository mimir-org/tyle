import { Aspect } from "@mimirorg/typelibrary-types";
import { Control, UseFormRegister } from "react-hook-form";
import { FormNodeLib } from "../../types/formNodeLib";
import { NodeFormAttributes } from "../attributes/NodeFormAttributes";
import { NodeFormPredefinedAttributes } from "../predefined-attributes/NodeFormPredefinedAttributes";
import { useTheme } from "styled-components";
import { Flexbox } from "../../../../complib/layouts";

export interface LocationNodeProps {
  control: Control<FormNodeLib>;
  register: UseFormRegister<FormNodeLib>;
}

export const LocationNode = ({ control, register }: LocationNodeProps) => {
  const theme = useTheme();

  return (
    <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
      <NodeFormAttributes control={control} register={register} aspects={[Aspect.Location]} />
      <NodeFormPredefinedAttributes control={control} register={register} aspects={[Aspect.Location]} />
    </Flexbox>
  );
};
