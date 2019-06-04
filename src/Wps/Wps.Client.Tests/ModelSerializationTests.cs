﻿using FluentAssertions;
using System.Text.RegularExpressions;
using Wps.Client.Models;
using Wps.Client.Models.Requests;
using Wps.Client.Services;
using Xunit;

namespace Wps.Client.Tests
{
    public class ModelSerializationTests : IClassFixture<XmlSerializationService>
    {

        private readonly XmlSerializationService _serializer;

        public ModelSerializationTests(XmlSerializationService serializer)
        {
            _serializer = serializer;
        }

        [Fact]
        public void SerializeGetCapabilitiesRequest_ValidRequestGiven_ShouldPass()
        {
            const string expectedXml = @"<?xml version=""1.0"" encoding=""utf-8""?><GetCapabilities xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" service=""WPS"" xmlns=""http://www.opengis.net/wps/2.0"" />";

            // Remove white spaces and new line characters. They do not change the actual (de)serialization of the XML.
            var trimmedExpectedXml = Regex.Replace(expectedXml, @"\s+", string.Empty);

            var request = new GetCapabilitiesRequest()
            {
                Service = "WPS"
            };

            var resultXml = _serializer.Serialize(request);
            var trimmedResult = Regex.Replace(resultXml, @"\s+", string.Empty);
            trimmedResult.Should().Be(trimmedExpectedXml);
        }

        [Fact]
        public void SerializeDescribeProcessRequest_ValidRequestGiven_ShouldPass()
        {
            const string expectedXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                         <wps:DescribeProcess xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:xli=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" service=""WPS"" version=""2.0.0"" lang=""en-US"" xmlns:wps=""http://www.opengis.net/wps/2.0"">
                                           <ows:Identifier>id1</ows:Identifier>
                                           <ows:Identifier>id2</ows:Identifier>
                                           <ows:Identifier>id3</ows:Identifier>
                                         </wps:DescribeProcess>";

            // Remove white spaces and new line characters. They do not change the actual (de)serialization of the XML.
            var trimmedExpectedXml = Regex.Replace(expectedXml, @"\s+", string.Empty);

            var request = new DescribeProcessRequest()
            {
                Identifiers = new[] { "id1", "id2", "id3" },
                Language = "en-US",
            };

            var resultXml = _serializer.Serialize(request);
            var trimmedResult = Regex.Replace(resultXml, @"\s+", string.Empty);
            trimmedResult.Should().Be(trimmedExpectedXml);
        }

        [Fact]
        public void SerializeGetStatusRequest_ValidRequestGiven_ShouldPass()
        {
            const string expectedXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                         <wps:GetStatus xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:xli=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" service=""WPS"" version=""2.0.0"" xmlns:wps=""http://www.opengis.net/wps/2.0"">
                                             <wps:JobID>testJobId</wps:JobID>
                                         </wps:GetStatus>";

            // Remove white spaces and new line characters. They do not change the actual (de)serialization of the XML.
            var trimmedExpectedXml = Regex.Replace(expectedXml, @"\s+", string.Empty);

            var request = new GetStatusRequest
            {
                JobId = "testJobId"
            };

            var resultXml = _serializer.Serialize(request);
            var trimmedResult = Regex.Replace(resultXml, @"\s+", string.Empty);
            trimmedResult.Should().Be(trimmedExpectedXml);
        }

    }
}