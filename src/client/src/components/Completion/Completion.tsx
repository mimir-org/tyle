import { Actionable, Box, Button, Text } from "@mimirorg/component-library";
import AuthContent from "components/AuthContent";
import { useTheme } from "styled-components";

type CompletionProps = Partial<Actionable> & {
  title: string;
  infoText: string;
  complete?: Partial<Actionable>;
};

const Completion = ({ title, infoText, complete }: CompletionProps) => {
  const theme = useTheme();

  return (
    <AuthContent
      title={title}
      firstRow={
        <Box
          display={"flex"}
          flexDirection={"column"}
          alignItems={"center"}
          alignSelf={"center"}
          gap={theme.tyle.spacing.xxxl}
          maxWidth={"300px"}
        >
          <Text textAlign={"center"}>{infoText}</Text>
          {complete?.actionable && <Button onClick={complete.onAction}>{complete.actionText}</Button>}
        </Box>
      }
    />
  );
};

export default Completion;
