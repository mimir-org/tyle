import { Aspect } from "@mimirorg/typelibrary-types";
import { Control, UseFormRegister } from "react-hook-form";
import { FormNodeLib } from "../../types/formNodeLib";
import { NodeFormAttributes } from "../attributes/NodeFormAttributes";
import { NodeFormTerminals } from "../terminals/NodeFormTerminals";
import { useTheme } from "styled-components";
import { Flexbox } from "../../../../complib/layouts";

export interface ProductNodeProps {
  control: Control<FormNodeLib>;
  register: UseFormRegister<FormNodeLib>;
}

export const ProductNode = ({ control, register }: ProductNodeProps) => {
  const theme = useTheme();

  return (
    <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
      <NodeFormTerminals control={control} />
      <NodeFormAttributes control={control} register={register} aspects={[Aspect.Product]} />
    </Flexbox>
  );
};
