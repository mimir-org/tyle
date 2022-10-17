import { useTheme } from "styled-components";
import { Button } from "../../../../../complib/buttons";
import { Box } from "../../../../../complib/layouts";
import { Text } from "../../../../../complib/text";
import { Actionable } from "../../../../../complib/types";
import { UnauthenticatedContent } from "../../../../app/components/unauthenticated/layout/UnauthenticatedContent";

type RegisterCompleteProps = Partial<Actionable> & {
  text: string;
};

export const RegisterComplete = ({ text, actionable, actionText, onAction }: RegisterCompleteProps) => {
  const theme = useTheme();

  return (
    <UnauthenticatedContent>
      <Box
        flex={1}
        display={"flex"}
        flexDirection={"column"}
        gap={theme.tyle.spacing.xxxl}
        justifyContent={"center"}
        alignItems={"center"}
        maxWidth={"300px"}
      >
        <Text textAlign={"center"}>{text}</Text>
        {actionable && <Button onClick={onAction}>{actionText}</Button>}
      </Box>
    </UnauthenticatedContent>
  );
};
