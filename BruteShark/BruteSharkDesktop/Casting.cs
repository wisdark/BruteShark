﻿using BruteForce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BruteSharkDesktop
{
    public static class Casting
    {
        public static BruteSharkDesktop.TransportLayerPacket CastProcessorTcpPacketToBruteSharkDesktopTcpPacket(PcapProcessor.TcpPacket tcpPacket)
        {
            return new BruteSharkDesktop.TransportLayerPacket()
            {
                Protocol = "TCP",
                SourceIp = tcpPacket.SourceIp,
                DestinationIp = tcpPacket.DestinationIp,
                SourcePort = tcpPacket.SourcePort,
                DestinationPort = tcpPacket.DestinationPort,
                Data = tcpPacket.Data
            };
        }

        public static BruteSharkDesktop.TransportLayerPacket CastProcessorUdpPacketToBruteSharkDesktopUdpPacket(PcapProcessor.UdpPacket udpPacket)
        {
            return new BruteSharkDesktop.TransportLayerPacket()
            {
                Protocol = "UDP",
                SourceIp = udpPacket.SourceIp,
                DestinationIp = udpPacket.DestinationIp,
                SourcePort = udpPacket.SourcePort,
                DestinationPort = udpPacket.DestinationPort,
                Data = udpPacket.Data
            };
        }

        public static BruteSharkDesktop.TransportLayerSession CastProcessorTcpSessionToBruteSharkDesktopTcpSession(PcapProcessor.TcpSession tcpSession)
        {
            return new BruteSharkDesktop.TransportLayerSession()
            {
                Protocol = "TCP",
                SourceIp = tcpSession.SourceIp,
                DestinationIp = tcpSession.DestinationIp,
                SourcePort = tcpSession.SourcePort,
                DestinationPort = tcpSession.DestinationPort,
                Data = tcpSession.Data,
                Packets = tcpSession.Packets.Select(p => CastProcessorTcpPacketToBruteSharkDesktopTcpPacket(p)).ToList()
            };
        }

        public static BruteSharkDesktop.TransportLayerSession CastProcessorUdpSessionToBruteSharkDesktopUdpSession(PcapProcessor.UdpSession udpSession)
        {
            return new BruteSharkDesktop.TransportLayerSession()
            {
                Protocol = "UDP",
                SourceIp = udpSession.SourceIp,
                DestinationIp = udpSession.DestinationIp,
                SourcePort = udpSession.SourcePort,
                DestinationPort = udpSession.DestinationPort,
                Data = udpSession.Data,
                Packets = udpSession.Packets.Select(p => CastProcessorUdpPacketToBruteSharkDesktopUdpPacket(p)).ToList()
            };
        }

        public static PcapAnalyzer.UdpPacket CastProcessorUdpPacketToAnalyzerUdpPacket(PcapProcessor.UdpPacket udpPacket)
        {
            return new PcapAnalyzer.UdpPacket()
            {
                SourceIp = udpPacket.SourceIp,
                DestinationIp = udpPacket.DestinationIp,
                SourcePort = udpPacket.SourcePort,
                DestinationPort = udpPacket.DestinationPort,
                Data = udpPacket.Data
            };
        }

        public static PcapAnalyzer.TcpPacket CastProcessorTcpPacketToAnalyzerTcpPacket(PcapProcessor.TcpPacket tcpPacket)
        {
            return new PcapAnalyzer.TcpPacket()
            {
                SourceIp = tcpPacket.SourceIp,
                DestinationIp = tcpPacket.DestinationIp,
                SourcePort = tcpPacket.SourcePort,
                DestinationPort = tcpPacket.DestinationPort,
                Data = tcpPacket.Data
            };
        }

        public static PcapAnalyzer.TcpSession CastProcessorTcpSessionToAnalyzerTcpSession(PcapProcessor.TcpSession tcpSession)
        {
            return new PcapAnalyzer.TcpSession()
            {
                SourceIp = tcpSession.SourceIp,
                DestinationIp = tcpSession.DestinationIp,
                SourcePort = tcpSession.SourcePort,
                DestinationPort = tcpSession.DestinationPort,
                Data = tcpSession.Data,
                Packets = tcpSession.Packets.Select(p => CastProcessorTcpPacketToAnalyzerTcpPacket(p)).ToList()
            };
        }


        public static BruteForce.Hash CastAnalyzerHashToBruteForceHash(PcapAnalyzer.NetworkHash hash)
        {
            BruteForce.Hash res = null;

            if (hash is PcapAnalyzer.HttpDigestHash)
            {
                res = CastAnalyzerHashToBruteForceHash(hash as PcapAnalyzer.HttpDigestHash);
            }
            else if (hash is PcapAnalyzer.CramMd5Hash)
            {
                res = CastAnalyzerHashToBruteForceHash(hash as PcapAnalyzer.CramMd5Hash);
            }
            else if (hash is PcapAnalyzer.NtlmHash)
            {
                res = CastAnalyzerHashToBruteForceHash(hash as PcapAnalyzer.NtlmHash);
            }
            else if (hash is PcapAnalyzer.KerberosHash)
            {
                res = CastAnalyzerHashToBruteForceHash(hash as PcapAnalyzer.KerberosHash);
            }
            else if (hash is PcapAnalyzer.KerberosTgsRepHash)
            {
                res = CastAnalyzerHashToBruteForceHash(hash as PcapAnalyzer.KerberosTgsRepHash);
            }
            else if (hash is PcapAnalyzer.KerberosAsRepHash)
            {
                res = CastAnalyzerHashToBruteForceHash(hash as PcapAnalyzer.KerberosAsRepHash);
            }
            else
            {
                throw new Exception("Hash type not supported");
            }

            return res;
        }

        // TODO: refactor this duplicate code
        private static Hash CastAnalyzerHashToBruteForceHash(PcapAnalyzer.KerberosTgsRepHash kerberosTgsRepHash)
        {
            return new BruteForce.KerberosTgsRepHash()
            {
                ServiceName = kerberosTgsRepHash.ServiceName,
                Realm = kerberosTgsRepHash.Realm,
                HashedData = kerberosTgsRepHash.Hash,
                Username = kerberosTgsRepHash.Username
            };
        }

        private static Hash CastAnalyzerHashToBruteForceHash(PcapAnalyzer.KerberosAsRepHash kerberosAsRepHash)
        {
            return new BruteForce.KerberosAsRepHash()
            {
                ServiceName = kerberosAsRepHash.ServiceName,
                Realm = kerberosAsRepHash.Realm,
                HashedData = kerberosAsRepHash.Hash,
                Username = kerberosAsRepHash.Username
            };
        }

        public static BruteForce.Hash CastAnalyzerHashToBruteForceHash(PcapAnalyzer.HttpDigestHash httpDigestHash)
        {
            return new BruteForce.HttpDigestHash()
            {
                ServerIp = httpDigestHash.Source,
                Qop = httpDigestHash.Qop,
                Realm = httpDigestHash.Realm,
                Nonce = httpDigestHash.Nonce,
                Uri = httpDigestHash.Uri,
                Cnonce = httpDigestHash.Cnonce,
                Nc = httpDigestHash.Nc,
                Username = httpDigestHash.Username,
                Method = httpDigestHash.Method,
                Response = httpDigestHash.Response
            };
        }

        public static BruteForce.Hash CastAnalyzerHashToBruteForceHash(PcapAnalyzer.NtlmHash ntlmHash)
        {
            return new BruteForce.NtlmHash()
            {
                Challenge = ntlmHash.Challenge,
                User = ntlmHash.User,
                Domain = ntlmHash.Domain,
                LmHash = ntlmHash.LmHash,
                NtHash = ntlmHash.NtHash,
                Workstation = ntlmHash.Workstation
            };
        }

        public static BruteForce.Hash CastAnalyzerHashToBruteForceHash(PcapAnalyzer.KerberosHash kerberosHash)
        {
            return new BruteForce.KerberosHash()
            {
                User = kerberosHash.User,
                Domain = kerberosHash.Domain,
                HashedData = kerberosHash.Hash
            };
        }

        public static BruteForce.Hash CastAnalyzerHashToBruteForceHash(PcapAnalyzer.CramMd5Hash cramMd5Hash)
        {
            return new BruteForce.CramMd5Hash()
            {
                HashedData = cramMd5Hash.Hash,
                Challenge = cramMd5Hash.Challenge
            };
        }
    }
}
