import { Flex } from "./Flex";
import { ComponentStory } from "@storybook/react";

export default {
  title: "Layouts/Flex",
  component: Flex,
  args: {
    gap: "16px",
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center",
  },
};

const Template: ComponentStory<typeof Flex> = (args) => (
  <Flex style={{ padding: "20px", backgroundColor: "hsla(0,0%,0%, 0.2)" }} {...args}>
    <div style={{ padding: "50px", backgroundColor: "#358537" }}>Generic flex item</div>
    <div style={{ padding: "50px", backgroundColor: "#70ff72" }}>Generic flex item</div>
    <div style={{ padding: "50px", backgroundColor: "#70ff72" }}>Generic flex item</div>
  </Flex>
);

export const Default = Template.bind({});
