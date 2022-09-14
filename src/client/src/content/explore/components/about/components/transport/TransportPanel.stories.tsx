import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockTransportItem } from "../../../../../../utils/mocks";
import { TransportPanel } from "./TransportPanel";

export default {
  title: "Content/Explore/About/TransportPanel",
  component: TransportPanel,
  args: {
    ...mockTransportItem(),
  },
} as ComponentMeta<typeof TransportPanel>;

const Template: ComponentStory<typeof TransportPanel> = (args) => <TransportPanel {...args} />;

export const Default = Template.bind({});
