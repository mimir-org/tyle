import { Spinner } from "complib/feedback";
import { Flexbox, Text } from "@mimirorg/component-library";
import { useTheme } from "styled-components";

interface RegisterProcessingProps {
  children?: string;
}

export const Processing = ({ children }: RegisterProcessingProps) => {
  const theme = useTheme();

  return (
    <Flexbox
      flex={1}
      flexDirection={"column"}
      justifyContent={"center"}
      alignItems={"center"}
      gap={theme.tyle.spacing.xl}
    >
      <Text variant={"title-medium"}>{children}</Text>
      <Spinner />
    </Flexbox>
  );
};
