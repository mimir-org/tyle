import { ComponentMeta } from "@storybook/react";
import { useTheme } from "styled-components";
import { Button } from "../../buttons";
import { Flexbox } from "../../layouts";
import { MotionCard } from "../../surfaces";
import { Text } from "../../text";
import { toast } from "./toast";
import { Toaster } from "./Toaster";

export default {
  title: "Data display/Toaster",
  component: Toaster,
} as ComponentMeta<typeof Toaster>;

export const Default = () => <Button onClick={() => toast("The default toast experience")}>Click to toast</Button>;

export const Success = () => <Button onClick={() => toast.success("A successful toast")}>Click to toast</Button>;

export const Error = () => <Button onClick={() => toast.error("A toast following an error")}>Click to toast</Button>;

export const WithCustomContent = () => {
  const theme = useTheme();

  return (
    <Button
      onClick={() =>
        toast.custom(
          <MotionCard variant={"elevated"} {...theme.tyle.animation.from("right", 400)}>
            <Flexbox gap={"16px"}>
              <div>
                <Text variant={"title-medium"}>Custom</Text>
                <Text variant={"body-medium"}>This toast takes JSX as param</Text>
              </div>
              <Button variant={"outlined"}>An action</Button>
            </Flexbox>
          </MotionCard>
        )
      }
    >
      Click to toast
    </Button>
  );
};
