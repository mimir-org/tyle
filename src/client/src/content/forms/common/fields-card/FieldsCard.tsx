import { Trash } from "@styled-icons/heroicons-outline";
import { ReactNode } from "react";
import { useTheme } from "styled-components";
import { Button } from "../../../../complib/buttons";
import { Box, Flexbox } from "../../../../complib/layouts";
import { Text } from "../../../../complib/text";
import { FieldsCardContainer } from "./FieldsCard.styled";

interface FieldsCardProps {
  index: number;
  removeText: string;
  onRemove: () => void;
  children?: ReactNode;
}

/**
 * Component shows a card with a number and a remove button. The children of the component are listed vertically.
 * Multiple FieldsCard components are meant to be used together to represent collections of inputs.
 *
 * @param index number shown on card
 * @param removeText text for screen-readers
 * @param onRemove action when clicking the remove button
 * @param children
 * @constructor
 */
export const FieldsCard = ({ index, removeText, onRemove, children }: FieldsCardProps) => {
  const theme = useTheme();

  return (
    <FieldsCardContainer>
      <Box display={"flex"} justifyContent={"space-between"} width={"100%"}>
        <Text variant={"title-medium"}>#{index}</Text>
        <Button variant={"outlined"} icon={<Trash />} iconOnly onClick={() => onRemove()}>
          {removeText}
        </Button>
      </Box>

      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.s}>
        {children}
      </Flexbox>
    </FieldsCardContainer>
  );
};
