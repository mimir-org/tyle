import { ComponentMeta } from "@storybook/react";
import { Button } from "complib/buttons";
import { toast } from "complib/data-display/toast/toast";
import { Toaster } from "complib/data-display/toast/Toaster";
import { Flexbox } from "complib/layouts";
import { MotionCard } from "complib/surfaces";
import { Text } from "complib/text";
import { useTheme } from "styled-components";

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
          <MotionCard {...theme.tyle.animation.from("right", 400)}>
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
